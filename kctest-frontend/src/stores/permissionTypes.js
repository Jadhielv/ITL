import { defineStore } from 'pinia'
import HttpService from '../services/HttpService'

const httpService = new HttpService()

function getErrorMessage (error) {
  return error instanceof Error ? error.message : String(error)
}

export const usePermissionTypesStore = defineStore('permissionTypes', {
  state: () => ({
    items: [],
    error: null
  }),
  actions: {
    async load () {
      this.error = null
      try {
        this.items = await httpService.get('permissionType')
        return true
      } catch (error) {
        this.error = `Unable to load permission types: ${getErrorMessage(error)}`
        return false
      }
    },
    async create (permissionType) {
      this.error = null
      try {
        const result = await httpService.post('permissionType', permissionType)
        this.items.push(result)
        return true
      } catch (error) {
        this.error = `Unable to create permission type: ${getErrorMessage(error)}`
        return false
      }
    },
    async update (permissionType) {
      this.error = null
      try {
        await httpService.put('permissionType', permissionType)
        const index = this.items.findIndex(item => item.id === permissionType.id)
        if (index !== -1) this.items[index] = permissionType
        return true
      } catch (error) {
        this.error = `Unable to update permission type: ${getErrorMessage(error)}`
        return false
      }
    },
    async remove (permissionTypeId) {
      this.error = null
      try {
        await httpService.delete('permissionType', permissionTypeId)
        this.items = this.items.filter(item => item.id !== permissionTypeId)
        return true
      } catch (error) {
        this.error = `Unable to delete permission type: ${getErrorMessage(error)}`
        return false
      }
    }
  }
})
