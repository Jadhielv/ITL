import { flushPromises, mount } from '@vue/test-utils'
import { createPinia, setActivePinia } from 'pinia'
import PermissionType from '@/components/PermissionType'
import { usePermissionTypesStore } from '@/stores/permissionTypes'

describe('PermissionType.vue', () => {
  async function createWrapper () {
    const pinia = createPinia()
    setActivePinia(pinia)
    const permissionTypesStore = usePermissionTypesStore()
    jest.spyOn(permissionTypesStore, 'load').mockResolvedValue(true)
    const wrapper = mount(PermissionType, { global: { plugins: [pinia] } })

    await flushPromises()
    return { wrapper, permissionTypesStore }
  }

  it('renders the permission type creation action', async () => {
    const { wrapper } = await createWrapper()
    expect(wrapper.text()).toContain('Add permission type')
    expect(wrapper.text()).toContain('All permission types')

    wrapper.vm.dismissCountDown = wrapper.vm.dismissSecs
    wrapper.vm.countDownChanged(4200)
    expect(wrapper.vm.countDownRemaining).toBe(4200)
    expect(wrapper.vm.dismissCountDown).toBe(wrapper.vm.dismissSecs)
  })

  it('waits for confirmation before deleting a permission type', async () => {
    const { wrapper, permissionTypesStore } = await createWrapper()
    const permissionType = { id: 3, description: 'Personal leave' }
    const remove = jest.spyOn(permissionTypesStore, 'remove').mockResolvedValue(true)

    wrapper.vm.deletePermissionType(permissionType)
    expect(wrapper.vm.showDeleteModal).toBe(true)
    expect(wrapper.vm.permissionTypeToDelete).toEqual(permissionType)
    expect(remove).not.toHaveBeenCalled()

    await wrapper.vm.confirmDeletePermissionType()
    expect(remove).toHaveBeenCalledWith(permissionType.id)
    expect(wrapper.vm.showDeleteModal).toBe(false)
    expect(wrapper.vm.dismissCountDown).toBe(wrapper.vm.dismissSecs)
    expect(wrapper.text()).toContain('Permission type deleted successfully')
  })

  it('does not show a success alert when deleting an assigned type fails', async () => {
    const { wrapper, permissionTypesStore } = await createWrapper()
    const remove = jest.spyOn(permissionTypesStore, 'remove').mockResolvedValue(false)

    wrapper.vm.deletePermissionType({ id: 1, description: 'Enfermedad' })
    await wrapper.vm.confirmDeletePermissionType()

    expect(remove).toHaveBeenCalledWith(1)
    expect(wrapper.vm.showDeleteModal).toBe(false)
    expect(wrapper.vm.dismissCountDown).toBe(0)
  })
})
