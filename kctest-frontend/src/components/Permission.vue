<template>
  <main class="container-xxl app-page">
    <header class="page-heading">
      <div>
        <p class="page-eyebrow">ACCESS MANAGEMENT</p>
        <h1>Permissions</h1>
        <p class="page-description">Manage employee permissions and the types assigned to them.</p>
      </div>
      <BButton class="app-primary-button" variant="primary" @click="openModal">
        <span class="button-plus" aria-hidden="true">+</span>
        Add permission
      </BButton>
    </header>

    <BModal v-model="showModal" :title="isEditing ? 'Edit permission' : 'Add permission'" no-footer @hidden="cleanModal">
      <BForm @submit.prevent="addOrEditPermission">
        <div v-if="errors.length" class="form-errors" role="alert">
          <strong>Please correct the following:</strong>
          <ul class="mb-0">
            <li v-for="(error, index) in errors" :key="index">{{ error }}</li>
          </ul>
        </div>
        <BFormGroup label="First name" label-for="permission-name">
          <BFormInput
            id="permission-name"
            v-model="permission.name"
            required
            placeholder="Enter first name"
            @keypress="isLetter"
          />
        </BFormGroup>
        <BFormGroup label="Last name" label-for="permission-last-name">
          <BFormInput
            id="permission-last-name"
            v-model="permission.lastName"
            required
            placeholder="Enter last name"
            @keypress="isLetter"
          />
        </BFormGroup>
        <BFormGroup label="Permission type" label-for="permission-type">
          <BFormSelect
            id="permission-type"
            v-model="permission.permissionType"
            :options="permissionTypeOptions"
            required
          />
        </BFormGroup>
        <BFormGroup label="Date" label-for="permission-date">
          <BFormInput
            id="permission-date"
            v-model="permission.date"
            type="date"
            required
          />
        </BFormGroup>
        <div class="modal-actions">
          <BButton variant="outline-secondary" @click="showModal = false">Cancel</BButton>
          <BButton class="app-primary-button" type="submit" variant="primary">Save permission</BButton>
        </div>
      </BForm>
    </BModal>

    <BModal
      v-model="showDeleteModal"
      title="Delete permission?"
      no-footer
      @hidden="clearDeleteConfirmation"
    >
      <p class="confirmation-copy">
        Are you sure you want to delete
        <strong>{{ permissionToDelete?.name }} {{ permissionToDelete?.lastName }}</strong>'s permission?
        This action cannot be undone.
      </p>
      <div class="modal-actions">
        <BButton variant="outline-secondary" @click="showDeleteModal = false">Cancel</BButton>
        <BButton class="app-danger-button" variant="danger" @click="confirmDeletePermission">
          Delete permission
        </BButton>
      </div>
    </BModal>

    <div class="summary-grid">
      <article class="summary-card">
        <div>
          <p class="summary-label">Total permissions</p>
          <p class="summary-value">{{ permissions.length }}</p>
        </div>
      </article>
      <article class="summary-card">
        <div>
          <p class="summary-label">Permission types</p>
          <p class="summary-value">{{ permissionTypes.length }}</p>
        </div>
      </article>
    </div>

    <BAlert
      v-if="permissionsStore.error || permissionTypesStore.error"
      :model-value="true"
      variant="danger"
      class="app-alert"
    >
      {{ permissionsStore.error || permissionTypesStore.error }}
    </BAlert>

    <section class="data-panel" aria-labelledby="permissions-table-title">
      <header class="panel-heading">
        <div>
          <h2 id="permissions-table-title">All permissions</h2>
          <p>Review and manage employee permission records.</p>
        </div>
        <span class="record-count">{{ permissions.length }} {{ permissions.length === 1 ? 'record' : 'records' }}</span>
      </header>
      <div class="table-responsive">
        <table class="table app-table align-middle mb-0">
          <thead>
            <tr>
              <th v-for="column in columns" :key="column.key" scope="col">{{ column.label }}</th>
              <th scope="col" class="actions-heading">Actions</th>
            </tr>
          </thead>
          <tbody v-if="permissions.length">
            <tr v-for="item in permissions" :key="item.id">
              <td v-for="column in columns" :key="column.key">
                <span v-if="column.key === 'permissionType'" class="type-badge">
                  {{ getElementValue(item[column.key]) }}
                </span>
                <span v-else>{{ column.key === 'date' ? formatDate(item.date) : item[column.key] }}</span>
              </td>
              <td class="row-actions">
                <BButton variant="outline-secondary" @click="editPermission(item)">Edit</BButton>
                <BButton variant="outline-danger" @click="deletePermission(item)">Delete</BButton>
              </td>
            </tr>
          </tbody>
          <tbody v-else>
            <tr>
              <td :colspan="columns.length + 1" class="empty-state">
                <span class="empty-state-icon" aria-hidden="true"></span>
                <strong>No permissions yet</strong>
                <span>Add a permission to see it listed here.</span>
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
        <span>Permission deleted successfully</span>
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
  BFormSelect,
  BModal,
  BProgress
} from 'bootstrap-vue-next'
import { storeToRefs } from 'pinia'
import { usePermissionTypesStore } from '../stores/permissionTypes'
import { usePermissionsStore } from '../stores/permissions'

export default {
  name: 'PermissionForm',
  components: {
    BAlert,
    BButton,
    BForm,
    BFormGroup,
    BFormInput,
    BFormSelect,
    BModal,
    BProgress
  },
  setup () {
    const permissionsStore = usePermissionsStore()
    const permissionTypesStore = usePermissionTypesStore()
    const { items: permissions } = storeToRefs(permissionsStore)
    const { items: permissionTypes } = storeToRefs(permissionTypesStore)
    return { permissionsStore, permissionTypesStore, permissions, permissionTypes }
  },
  data () {
    return {
      showModal: false,
      showDeleteModal: false,
      columns: [
        { label: 'Employee', key: 'name' },
        { label: 'Last name', key: 'lastName' },
        { label: 'Permission type', key: 'permissionType' },
        { label: 'Date', key: 'date' }
      ],
      errors: [],
      permission: { id: undefined, name: '', lastName: '', permissionType: null, date: '' },
      permissionToDelete: null,
      isEditing: false,
      dismissSecs: 5000,
      dismissCountDown: 0,
      countDownRemaining: 0
    }
  },
  computed: {
    permissionTypeOptions () {
      return [
        { value: null, text: 'Please select an option' },
        ...this.permissionTypes.map(item => ({ text: item.description, value: item }))
      ]
    }
  },
  methods: {
    formatDate (value) {
      if (!value) return ''
      return new Intl.DateTimeFormat('en-US', {
        month: 'short',
        day: 'numeric',
        year: 'numeric',
        timeZone: 'UTC'
      }).format(new Date(value))
    },
    getElementValue (element) {
      return element?.description || element
    },
    openModal () {
      this.cleanModal()
      this.showModal = true
    },
    async addOrEditPermission () {
      if (!this.checkForm()) return

      const saved = this.isEditing
        ? await this.permissionsStore.update(this.permission)
        : await this.permissionsStore.create(this.permission)

      if (saved) this.showModal = false
    },
    editPermission (permission) {
      this.permission = {
        ...permission,
        permissionType: permission.permissionType ? { ...permission.permissionType } : null
      }
      this.isEditing = true
      this.showModal = true
    },
    deletePermission (permission) {
      this.permissionToDelete = permission
      this.showDeleteModal = true
    },
    async confirmDeletePermission () {
      if (!this.permissionToDelete) return

      const deleted = await this.permissionsStore.remove(this.permissionToDelete.id)
      this.showDeleteModal = false
      if (deleted) this.showAlert()
    },
    clearDeleteConfirmation () {
      this.permissionToDelete = null
    },
    cleanModal () {
      this.permission = { id: undefined, name: '', lastName: '', permissionType: null, date: '' }
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
      if (!this.permission.name) this.errors.push('Name required.')
      if (!this.permission.lastName) this.errors.push('Last Name required.')
      if (!this.permission.permissionType) this.errors.push('Permission type required.')
      if (!this.permission.date) this.errors.push('Date required.')
      return this.errors.length === 0
    }
  },
  async mounted () {
    await Promise.all([
      this.permissionsStore.load(),
      this.permissionTypesStore.load()
    ])
  }
}
</script>
