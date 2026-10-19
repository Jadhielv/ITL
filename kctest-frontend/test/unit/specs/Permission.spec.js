import { flushPromises, mount } from '@vue/test-utils'
import { createPinia, setActivePinia } from 'pinia'
import Permission from '@/components/Permission'
import { usePermissionTypesStore } from '@/stores/permissionTypes'
import { usePermissionsStore } from '@/stores/permissions'

describe('Permission.vue', () => {
  async function createWrapper () {
    const pinia = createPinia()
    setActivePinia(pinia)
    const permissionsStore = usePermissionsStore()
    const permissionTypesStore = usePermissionTypesStore()
    jest.spyOn(permissionsStore, 'load').mockResolvedValue(true)
    jest.spyOn(permissionTypesStore, 'load').mockResolvedValue(true)
    const wrapper = mount(Permission, { global: { plugins: [pinia] } })

    await flushPromises()
    return { wrapper, permissionsStore }
  }

  it('renders the permission creation action', async () => {
    const { wrapper } = await createWrapper()

    expect(wrapper.text()).toContain('Add permission')
    expect(wrapper.text()).toContain('All permissions')
    expect(wrapper.vm.formatDate('2026-10-19T00:00:00Z')).toBe('Oct 19, 2026')

    wrapper.vm.dismissCountDown = wrapper.vm.dismissSecs
    wrapper.vm.countDownChanged(4200)
    expect(wrapper.vm.countDownRemaining).toBe(4200)
    expect(wrapper.vm.dismissCountDown).toBe(wrapper.vm.dismissSecs)
  })

  it('waits for confirmation before deleting a permission', async () => {
    const { wrapper, permissionsStore } = await createWrapper()
    const permission = { id: 7, name: 'Ada', lastName: 'Lovelace' }
    const remove = jest.spyOn(permissionsStore, 'remove').mockResolvedValue(true)

    wrapper.vm.deletePermission(permission)
    expect(wrapper.vm.showDeleteModal).toBe(true)
    expect(wrapper.vm.permissionToDelete).toEqual(permission)
    expect(remove).not.toHaveBeenCalled()

    await wrapper.vm.confirmDeletePermission()
    expect(remove).toHaveBeenCalledWith(permission.id)
    expect(wrapper.vm.showDeleteModal).toBe(false)
    expect(wrapper.vm.dismissCountDown).toBe(wrapper.vm.dismissSecs)
    expect(wrapper.text()).toContain('Permission deleted successfully')
  })

  it('does not show a success alert when deletion fails', async () => {
    const { wrapper, permissionsStore } = await createWrapper()
    const remove = jest.spyOn(permissionsStore, 'remove').mockResolvedValue(false)

    wrapper.vm.deletePermission({ id: 8, name: 'Grace', lastName: 'Hopper' })
    await wrapper.vm.confirmDeletePermission()

    expect(remove).toHaveBeenCalledWith(8)
    expect(wrapper.vm.showDeleteModal).toBe(false)
    expect(wrapper.vm.dismissCountDown).toBe(0)
  })
})
