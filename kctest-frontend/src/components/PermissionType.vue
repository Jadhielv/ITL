<template>
  <main class="container-xxl app-page">
    <header class="page-heading">
      <div>
        <p class="page-eyebrow">SETUP</p>
        <h1>Permission types</h1>
        <p class="page-description">Organize the categories used to classify employee permissions.</p>
      </div>
      <BButton class="app-primary-button" variant="primary" @click="openModal">
        <span class="button-plus" aria-hidden="true">+</span>
        Add permission type
      </BButton>
    </header>

    <BModal v-model="showModal" :title="isEditing ? 'Edit permission type' : 'Add permission type'" no-footer @hidden="cleanModal">
      <BForm @submit.prevent="addOrEditPermissionType">
        <div v-if="errors.length" class="form-errors" role="alert">
          <strong>Please correct the following:</strong>
          <ul class="mb-0">
            <li v-for="(error, index) in errors" :key="index">{{ error }}</li>
          </ul>
        </div>
        <BFormGroup label="Description" label-for="permission-type-description">
          <BFormInput
            id="permission-type-description"
            v-model="permissionType.description"
            required
            placeholder="Enter a type name"
            @keypress="isLetter"
          />
        </BFormGroup>
        <div class="modal-actions">
          <BButton variant="outline-secondary" @click="showModal = false">Cancel</BButton>
          <BButton class="app-primary-button" type="submit" variant="primary">Save type</BButton>
        </div>
      </BForm>
    </BModal>

    <BModal
      v-model="showDeleteModal"
      title="Delete permission type?"
      no-footer
      @hidden="clearDeleteConfirmation"
    >
      <p class="confirmation-copy">
        Are you sure you want to delete
        <strong>{{ permissionTypeToDelete?.description }}</strong>?
        If it is assigned to existing permissions, deletion will be rejected.
      </p>
      <div class="modal-actions">
        <BButton variant="outline-secondary" @click="showDeleteModal = false">Cancel</BButton>
        <BButton class="app-danger-button" variant="danger" @click="confirmDeletePermissionType">
          Delete type
        </BButton>
      </div>
    </BModal>

    <div class="summary-grid summary-grid-single">
      <article class="summary-card">
        <div>
          <p class="summary-label">Available types</p>
          <p class="summary-value">{{ permissionTypes.length }}</p>
        </div>
      </article>
    </div>

    <BAlert v-if="permissionTypesStore.error" :model-value="true" variant="danger" class="app-alert">
      {{ permissionTypesStore.error }}
    </BAlert>

    <section class="data-panel" aria-labelledby="permission-types-table-title">
      <header class="panel-heading">
        <div>
          <h2 id="permission-types-table-title">All permission types</h2>
          <p>Manage the categories available when creating a permission.</p>
        </div>
        <span class="record-count">{{ permissionTypes.length }} {{ permissionTypes.length === 1 ? 'type' : 'types' }}</span>
      </header>
      <div class="table-responsive">
        <table class="table app-table align-middle mb-0">
          <thead>
            <tr>
              <th v-for="column in columns" :key="column" scope="col">{{ column }}</th>
              <th scope="col" class="actions-heading">Actions</th>
            </tr>
          </thead>
          <tbody v-if="permissionTypes.length">
            <tr v-for="item in permissionTypes" :key="item.id">
              <td><span class="type-badge">{{ item.description }}</span></td>
              <td class="row-actions">
                <BButton variant="outline-secondary" @click="editPermissionType(item)">Edit</BButton>
                <BButton variant="outline-danger" @click="deletePermissionType(item)">Delete</BButton>
              </td>
            </tr>
          </tbody>
          <tbody v-else>
            <tr>
              <td :colspan="columns.length + 1" class="empty-state">
                <span class="empty-state-icon" aria-hidden="true"></span>
                <strong>No permission types yet</strong>
                <span>Add a type to use it when creating permissions.</span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </section>

    <BAlert
      v-model="dismissCountDown"
      dismissible
      variant="success"
      class="app-toast app-toast-success"
      @close-countdown="countDownChanged"
    >
      <div class="toast-content">
        <span class="toast-check" aria-hidden="true">✓</span>
        <span>Permission type deleted successfully</span>
      </div>
      <BProgress class="toast-progress" variant="success" :max="dismissSecs" :value="countDownRemaining" />
    </BAlert>
  </main>
</template>

<script>
import {
  BAlert,
  BButton,
  BForm,
  BFormGroup,
  BFormInput,
  BModal,
  BProgress
} from 'bootstrap-vue-next'
import { storeToRefs } from 'pinia'
import { usePermissionTypesStore } from '../stores/permissionTypes'

export default {
  name: 'PermissionType',
  components: {
    BAlert,
    BButton,
    BForm,
    BFormGroup,
    BFormInput,
    BModal,
    BProgress
  },
  setup () {
    const permissionTypesStore = usePermissionTypesStore()
    const { items: permissionTypes } = storeToRefs(permissionTypesStore)
    return { permissionTypesStore, permissionTypes }
  },
  data () {
    return {
      showModal: false,
      showDeleteModal: false,
      columns: ['Permission type'],
      errors: [],
      permissionType: { id: undefined, description: '' },
      permissionTypeToDelete: null,
      isEditing: false,
      dismissSecs: 5000,
      dismissCountDown: 0,
      countDownRemaining: 0
    }
  },
  methods: {
    openModal () {
      this.cleanModal()
      this.showModal = true
    },
    async addOrEditPermissionType () {
      if (!this.checkForm()) return

      const saved = this.isEditing
        ? await this.permissionTypesStore.update(this.permissionType)
        : await this.permissionTypesStore.create(this.permissionType)

      if (saved) this.showModal = false
    },
    editPermissionType (permissionType) {
      this.permissionType = { ...permissionType }
      this.isEditing = true
      this.showModal = true
    },
    deletePermissionType (permissionType) {
      this.permissionTypeToDelete = permissionType
      this.showDeleteModal = true
    },
    async confirmDeletePermissionType () {
      if (!this.permissionTypeToDelete) return

      const deleted = await this.permissionTypesStore.remove(this.permissionTypeToDelete.id)
      this.showDeleteModal = false
      if (deleted) this.showAlert()
    },
    clearDeleteConfirmation () {
      this.permissionTypeToDelete = null
    },
    cleanModal () {
      this.permissionType = { id: undefined, description: '' }
      this.isEditing = false
      this.errors = []
    },
    countDownChanged (remainingMs) {
      this.countDownRemaining = remainingMs
    },
    showAlert () {
      this.dismissCountDown = this.dismissSecs
    },
    isLetter (event) {
      if (!/^[A-Za-z\s]$/.test(event.key)) event.preventDefault()
    },
    checkForm () {
      this.errors = []
      if (!this.permissionType.description) this.errors.push('Description required.')
      return this.errors.length === 0
    }
  },
  async mounted () {
    await this.permissionTypesStore.load()
  }
}
</script>
