<template>
  <div class="container mt-3">
    <div class="row">
      <div class="col-4">
          <b-button class="mb-2 btn btn-secondary" v-b-modal="'permissionForm'">Add new permission</b-button>
          <b-modal id="permissionForm" hide-footer v-if='showModal' @hidden="cleanModal()">
             <b-form>
               <p v-if="errors.length">
                <b>Please correct the following error(s):</b>
                <ul>
                  <li v-for='error in errors' v-bind:key='error.id'>{{ error }}</li>
                </ul>
              </p>
                <b-form-group class="font-weight-bold" id="input-group-1" label="Name:" label-for="input-1" description="Name">
                  <b-form-input id="input-1"
                    v-model="permission.name"
                    type="text"
                    v-on:keypress='isLetter($event)'
                    required placeholder="Enter name"></b-form-input>
                </b-form-group>

                <b-form-group class="font-weight-bold" id="input-group-1" label="Last Name:" label-for="input-1" description="Last Name">
                  <b-form-input id="input-1"
                    v-model="permission.lastName"
                    type="text"
                    v-on:keypress='isLetter($event)'
                    required placeholder="Enter lastName"></b-form-input>
                </b-form-group>

                <b-form-group class="font-weight-bold" id="input-group-1" label="Permission type:" label-for="input-1" description="Permission type">
                  <b-form-select id="input-3" v-model="permission.permissionType" :options="permissionTypes" required>
                  </b-form-select>
                </b-form-group>

                <b-form-group class="font-weight-bold" id="input-group-1" label="Date:" label-for="input-1" description="Date">
                  <b-form-input id="input-1"
                    v-model="permission.date"
                    type="date"
                    required placeholder="Enter date"></b-form-input>
                </b-form-group>

                <b-button class="float-right btn btn-secondary" v-on:click='addOrEditPermission($event)' variant="secondary">Save</b-button>
              </b-form>
          </b-modal>
      </div>
      <div class="col-12">
          <table class="table table-striped table-bordered">
            <thead>
              <tr>
                <th v-for='column of columns' v-bind:key='column.id' scope="col">{{ column }}</th>
                <th scope="col">Actions</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for='permission of permissions' v-bind:key='permission.id'>
                <th scope="row" v-for='column of columns' v-bind:key='column.id'>{{ getElementValue(permission[lowerCaseFirstLetter(column)]) }}</th>
                <th>
                  <b-button variant="btn btn-success" v-b-modal="'permissionForm'" v-on:click='editPermission(permission)'>Edit</b-button>
                  <b-button variant="btn btn-danger" v-on:click='deletePermission(permission)'>Delete</b-button>
                </th>
              </tr>
            </tbody>
          </table>
      </div>
    </div>
    <b-alert :show="dismissCountDown" dismissible variant="success" @dismissed="dismissCountDown=0"
      @dismiss-count-down="countDownChanged">
      <p>Permission deleted</p>
      <b-progress variant="warning" :max="dismissSecs" :value="dismissCountDown" height="4px"></b-progress>
    </b-alert>
  </div>
</template>

<script>
import HttpService from './../services/HttpService.js'
import moment from 'moment'

const httpService = new HttpService()

export default {
  name: 'permission',
  methods: {
    lowerCaseFirstLetter: function (str = '') {
      return str.charAt(0).toLowerCase() + str.slice(1)
    },
    getElementValue: function (element) {
      return element?.description || element
    },
    addOrEditPermission: async function (e) {
      if (this.checkForm(e)) {
        if (!this.isEditing) {
          const result = await httpService.post('permission', this.permission)
          this.permissions = [...this.permissions, this.permission]
          if (result.success) {
            this.$bvModal.hide('permissionForm')
            this.cleanModal()
            this.updatePermissions()
          } else {
            this.permissions = this.permissions.pop()
          }
        } else {
          const result = await httpService.put('permission', this.permission)
          if (result.success) {
            const index = this.permissions.indexOf(this.permission)
            this.permissions[index] = this.permission
            this.$bvModal.hide('permissionForm')
            this.cleanModal()
          } else { }
        }
      }
    },
    editPermission: function (permission) {
      this.permission = permission
      this.isEditing = true
    },
    deletePermission: async function (permission) {
      const result = await httpService.delete('permission', permission.id)
      if (result.success) {
        const index = this.permissions.indexOf(permission)
        this.permissions.splice(index, 1)
        this.showAlert()
      } else { }
    },
    cleanModal: function () {
      this.permission = { id: undefined, name: '', lastName: '', permissionType: null, date: '' }
      this.isEditing = false
      this.errors = []
    },
    countDownChanged: function (dismissCountDown) {
      this.dismissCountDown = dismissCountDown
    },
    showAlert: function () {
      this.dismissCountDown = this.dismissSecs
    },
    updatePermissions: async function () {
      const permissionsRequestResult = await httpService.get('permission')

      if (typeof permissionsRequestResult !== 'undefined') {
        if (permissionsRequestResult.success) {
          permissionsRequestResult.list.forEach(function (item) {
            item.date = moment(String(item.date)).format('YYYY-MM-DD')
          })

          this.permissions = permissionsRequestResult.list
        }
      } else {
        alert('Service Unavailable: Network Error')
      }
    },
    isLetter: function (e) {
      let char = String.fromCharCode(e.keyCode)
      if (/^[A-Za-z\s]+$/.test(char)) return true
      else e.preventDefault()
    },
    checkForm: function (e) {
      this.errors = []

      if (!this.permission.name) {
        this.errors.push('Name required.')
      }
      if (!this.permission.lastName) {
        this.errors.push('Last Name required.')
      }
      if (!this.permission.permissionType) {
        this.errors.push('Permission type required.')
      }
      if (!this.permission.date) {
        this.errors.push('Date required.')
      }

      if (!this.errors.length) {
        return true
      }

      e.preventDefault()
    }
  },
  data: function () {
    return {
      showModal: true,
      columns: ['Name', 'LastName', 'PermissionType', 'Date'],
      permissions: [],
      errors: [],
      permission: { id: undefined, name: '', lastName: '', permissionType: null, date: '' },
      permissionTypes: [ { value: null, text: 'Please select an option' } ],
      isEditing: false,
      dismissSecs: 5,
      dismissCountDown: 0,
      showDismissibleAlert: false
    }
  },
  created: async function () {
    const permissionsRequestResult = await httpService.get('permission')

    if (typeof permissionsRequestResult !== 'undefined') {
      if (permissionsRequestResult.success) {
        permissionsRequestResult.list.forEach(function (item) {
          item.date = moment(String(item.date)).format('YYYY-MM-DD')
        })

        this.permissions = permissionsRequestResult.list
      }
    } else {
      alert('Service Unavailable: Network Error')
    }

    const permissionTypesRequestResult = await httpService.get('permissionType')

    if (typeof permissionTypesRequestResult !== 'undefined') {
      if (permissionTypesRequestResult.success) {
        this.permissionTypes = [ ...this.permissionTypes, ...permissionTypesRequestResult.list.map((x) => {
          return { text: x.description, value: x }
        })]
      }
    } else {
      alert('Service Unavailable: Network Error')
    }
  }
}
</script>
