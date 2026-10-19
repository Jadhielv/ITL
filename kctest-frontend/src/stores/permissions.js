import { defineStore } from 'pinia'
import moment from 'moment'
import HttpService from '../services/HttpService'

const httpService = new HttpService()

function getErrorMessage (error) {
  return error instanceof Error ? error.message : String(error)
}

function normalizePermission (permission) {
  return {
    ...permission,
    date: moment(String(permission.date)).format('YYYY-MM-DD')
  }
}

export const usePermissionsStore = defineStore('permissions', {
  state: () => ({
    items: [],
    error: null
  }),
  actions: {
    async load () {
      this.error = null
      try {
        this.items = (await httpService.get('permission')).map(normalizePermission)
        return true
      } catch (error) {
        this.error = `Unable to load permissions: ${getErrorMessage(error)}`
        return false
      }
    },
    async create (permission) {
      this.error = null
      try {
        const result = await httpService.post('permission', permission)
        this.items.push(normalizePermission(result))
        return true
      } catch (error) {
        this.error = `Unable to create permission: ${getErrorMessage(error)}`
        return false
      }
    },
    async update (permission) {
      this.error = null
      try {
        await httpService.put('permission', permission)
        const index = this.items.findIndex(item => item.id === permission.id)
        if (index !== -1) this.items[index] = normalizePermission(permission)
        return true
      } catch (error) {
        this.error = `Unable to update permission: ${getErrorMessage(error)}`
        return false
      }
    },
    async remove (permissionId) {
      this.error = null
      try {
        await httpService.delete('permission', permissionId)
        this.items = this.items.filter(item => item.id !== permissionId)
        return true
      } catch (error) {
        this.error = `Unable to delete permission: ${getErrorMessage(error)}`
        return false
      }
    }
  }
})
