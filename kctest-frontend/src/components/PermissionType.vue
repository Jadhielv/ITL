<template>
  <div class="container mt-3">
    <div class="row">
      <div class="col-4">
        <BButton class="mb-2" variant="secondary" @click="openModal">
          Add new permission type
        </BButton>
        <BModal v-model="showModal" title="Permission type" hide-footer @hidden="cleanModal">
          <BForm @submit.prevent="addOrEditPermissionType">
            <p v-if="errors.length">
              <strong>Please correct the following error(s):</strong>
            </p>
            <ul v-if="errors.length">
              <li v-for="(error, index) in errors" :key="index">{{ error }}</li>
            </ul>
            <BFormGroup label="Description:" label-for="permission-type-description">
              <BFormInput
                id="permission-type-description"
                v-model="permissionType.description"
                required
                placeholder="Enter description"
                @keypress="isLetter"
              />
            </BFormGroup>
            <BButton class="float-end" type="submit" variant="secondary">Save</BButton>
          </BForm>
        </BModal>
      </div>
      <div class="col-12">
        <BAlert v-if="permissionTypesStore.error" :model-value="true" variant="danger">
          {{ permissionTypesStore.error }}
        </BAlert>
        <table class="table table-striped table-bordered" style="width: 40% !important">
          <thead>
            <tr>
              <th v-for="column in columns" :key="column" scope="col">{{ column }}</th>
              <th scope="col">Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in permissionTypes" :key="item.id">
              <th v-for="column in columns" :key="column" scope="row">
                {{ getElementValue(item[lowerCaseFirstLetter(column)]) }}
              </th>
              <td>
                <BButton class="me-1" variant="success" @click="editPermissionType(item)">Edit</BButton>
                <BButton variant="danger" @click="deletePermissionType(item)">Delete</BButton>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <BAlert
      v-model="dismissCountDown"
      dismissible
      variant="success"
      @close-countdown="countDownChanged"
    >
      <p>Permission type deleted</p>
      <BProgress variant="warning" :max="dismissSecs" :value="dismissCountDown" height="4px" />
    </BAlert>
  </div>
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
      columns: ['Description'],
      errors: [],
      permissionType: { id: undefined, description: '' },
      isEditing: false,
      dismissSecs: 5000,
      dismissCountDown: 0
    }
  },
  methods: {
    lowerCaseFirstLetter (str = '') {
      return str.charAt(0).toLowerCase() + str.slice(1)
    },
    getElementValue (element) {
      return element?.description || element
    },
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
    async deletePermissionType (permissionType) {
      if (await this.permissionTypesStore.remove(permissionType.id)) this.showAlert()
    },
    cleanModal () {
      this.permissionType = { id: undefined, description: '' }
      this.isEditing = false
      this.errors = []
    },
    countDownChanged (dismissCountDown) {
      this.dismissCountDown = dismissCountDown
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
