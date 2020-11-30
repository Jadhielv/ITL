<template>
  <div class="container mt-3">
    <div class="row">
      <div class="col-4">
          <b-button class="mb-2 btn btn-secondary" v-b-modal="'permissionTypeForm'">Add new permission type</b-button>
          <b-modal id="permissionTypeForm" hide-footer v-if='showModal' @hidden="cleanModal()">
             <b-form>
                <b-form-group class="font-weight-bold" id="input-group-1" label="Description:" label-for="input-1" description="Description">
                  <b-form-input id="input-1" v-model="permissionType.description" type="text" v-on:keypress='isLetter($event)' required placeholder="Enter description"></b-form-input>
                </b-form-group>

                <b-button class="float-right btn btn-secondary" v-on:click='addOrEditPermissionType()' variant="secondary">Save</b-button>
              </b-form>
          </b-modal>
      </div>
      <div class="col-12">
          <table class="table table-striped table-bordered" style="width: 40% !important">
            <thead>
              <tr>
                <th v-for='column of columns' v-bind:key='column.id' scope="col">{{ column }}</th>
                <th scope="col">Actions</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for='permissionType of permissionTypes' v-bind:key='permissionType.id'>
                <th scope="row" v-for='column of columns' v-bind:key='column.id'>{{ getElementValue(permissionType[lowerCaseFirstLetter(column)]) }}</th>
                <th>
                  <b-button variant="btn btn-success" v-b-modal="'permissionTypeForm'" v-on:click='editPermissionType(permissionType)'>Edit</b-button>
                  <b-button variant="btn btn-danger" v-on:click='deletePermissionType(permissionType)'>Delete</b-button>
                </th>
              </tr>
            </tbody>
          </table>
      </div>
    </div>
    <b-alert :show="dismissCountDown" dismissible variant="success" @dismissed="dismissCountDown=0"
      @dismiss-count-down="countDownChanged">
      <p>Permission type deleted</p>
      <b-progress variant="warning" :max="dismissSecs" :value="dismissCountDown" height="4px"></b-progress>
    </b-alert>
  </div>
</template>

<script>
import HttpService from './../services/HttpService.js'

const httpService = new HttpService()

export default {
  name: 'permission-type',
  methods: {
    lowerCaseFirstLetter: function (str = '') {
      return str.charAt(0).toLowerCase() + str.slice(1)
    },
    getElementValue: function (element) {
      return element?.description || element
    },
    addOrEditPermissionType: async function () {
      if (!this.isEditing) {
        const result = await httpService.post('permissionType', this.permissionType)
        this.permissionTypes = [...this.permissionTypes, this.permissionType]
        if (result.success) {
          this.$bvModal.hide('permissionTypeForm')
          this.cleanModal()
          this.updatePermissionTypes()
        } else {
          this.permissionTypes = this.permissionTypes.pop()
        }
      } else {
        const result = await httpService.put('permissionType', this.permissionType)
        if (result.success) {
          const index = this.permissionTypes.indexOf(this.permissionType)
          this.permissionTypes[index] = this.permissionType
          this.$bvModal.hide('permissionTypeForm')
          this.cleanModal()
        } else { }
      }
    },
    editPermissionType: function (permissionType) {
      this.permissionType = permissionType
      this.isEditing = true
    },
    deletePermissionType: async function (permissionType) {
      const result = await httpService.delete('permissionType', permissionType.id)
      if (result.success) {
        const index = this.permissionTypes.indexOf(permissionType)
        this.permissionTypes.splice(index, 1)
        this.showAlert()
      } else { }
    },
    cleanModal: function () {
      this.permissionType = { id: undefined, description: '' }
      this.isEditing = false
    },
    countDownChanged: function (dismissCountDown) {
      this.dismissCountDown = dismissCountDown
    },
    showAlert: function () {
      this.dismissCountDown = this.dismissSecs
    },
    updatePermissionTypes: async function () {
      const permissionTypesRequestResult = await httpService.get('permissionType')

      if (typeof permissionTypesRequestResult !== 'undefined') {
        if (permissionTypesRequestResult.success) {
          this.permissionTypes = permissionTypesRequestResult.list
        }
      } else {
        alert('Service Unavailable: Network Error')
      }
    },
    isLetter: function (e) {
      let char = String.fromCharCode(e.keyCode)
      if (/^[A-Za-z]+$/.test(char)) return true
      else e.preventDefault()
    }
  },
  data: function () {
    return {
      showModal: true,
      columns: ['Description'],
      permissionTypes: [],
      permissionType: { id: undefined, description: '' },
      isEditing: false,
      dismissSecs: 5,
      dismissCountDown: 0,
      showDismissibleAlert: false
    }
  },
  created: async function () {
    const permissionTypesRequestResult = await httpService.get('permissionType')

    if (typeof permissionTypesRequestResult !== 'undefined') {
      if (permissionTypesRequestResult.success) {
        this.permissionTypes = permissionTypesRequestResult.list
      }
    } else {
      alert('Service Unavailable: Network Error')
    }
  }
}
</script>
