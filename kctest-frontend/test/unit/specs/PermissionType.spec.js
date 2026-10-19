import { flushPromises, mount } from '@vue/test-utils'
import { createPinia } from 'pinia'
import PermissionType from '@/components/PermissionType'

describe('PermissionType.vue', () => {
  it('renders the permission type creation action', async () => {
    const wrapper = mount(PermissionType, {
      global: { plugins: [createPinia()] }
    })

    await flushPromises()
    expect(wrapper.text()).toContain('Add new permission type')

    wrapper.vm.dismissCountDown = wrapper.vm.dismissSecs
    wrapper.vm.countDownChanged(4200)
    expect(wrapper.vm.countDownRemaining).toBe(4200)
    expect(wrapper.vm.dismissCountDown).toBe(wrapper.vm.dismissSecs)
  })
})
