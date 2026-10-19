<template>
  <div class="container mt-3">
    <div class="row">
      <div class="col-4">
        <BButton class="mb-2" variant="secondary" @click="openModal">
          Add new permission
        </BButton>
        <BModal v-model="showModal" title="Permission" hide-footer @hidden="cleanModal">
          <BForm @submit.prevent="addOrEditPermission">
            <p v-if="errors.length">
              <strong>Please correct the following error(s):</strong>
            </p>
            <ul v-if="errors.length">
              <li v-for="(error, index) in errors" :key="index">{{ error }}</li>
            </ul>
            <BFormGroup label="Name:" label-for="permission-name">
              <BFormInput
                id="permission-name"
                v-model="permission.name"
                required
                placeholder="Enter name"
                @keypress="isLetter"
              />
            </BFormGroup>
            <BFormGroup label="Last Name:" label-for="permission-last-name">
              <BFormInput
                id="permission-last-name"
                v-model="permission.lastName"
                required
                placeholder="Enter last name"
                @keypress="isLetter"
              />
            </BFormGroup>
            <BFormGroup label="Permission type:" label-for="permission-type">
              <BFormSelect
                id="permission-type"
                v-model="permission.permissionType"
                :options="permissionTypeOptions"
                required
              />
            </BFormGroup>
            <BFormGroup label="Date:" label-for="permission-date">
              <BFormInput
                id="permission-date"
                v-model="permission.date"
                type="date"
                required
              />
            </BFormGroup>
            <BButton class="float-end" type="submit" variant="secondary">Save</BButton>
          </BForm>
        </BModal>
      </div>
      <div class="col-12">
        <BAlert
          v-if="permissionsStore.error || permissionTypesStore.error"
          :model-value="true"
          variant="danger"
        >
          {{ permissionsStore.error || permissionTypesStore.error }}
        </BAlert>
        <table class="table table-striped table-bordered">
          <thead>
            <tr>
              <th v-for="column in columns" :key="column" scope="col">{{ column }}</th>
              <th scope="col">Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in permissions" :key="item.id">
              <th v-for="column in columns" :key="column" scope="row">
                {{ getElementValue(item[lowerCaseFirstLetter(column)]) }}
              </th>
              <td>
                <BButton class="me-1" variant="success" @click="editPermission(item)">Edit</BButton>
                <BButton variant="danger" @click="deletePermission(item)">Delete</BButton>
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
      <p>Permission deleted</p>
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
      columns: ['Name', 'LastName', 'PermissionType', 'Date'],
      errors: [],
      permission: { id: undefined, name: '', lastName: '', permissionType: null, date: '' },
      isEditing: false,
      dismissSecs: 5000,
      dismissCountDown: 0
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
    async deletePermission (permission) {
      if (await this.permissionsStore.remove(permission.id)) this.showAlert()
    },
    cleanModal () {
      this.permission = { id: undefined, name: '', lastName: '', permissionType: null, date: '' }
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
