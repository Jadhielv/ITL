import { flushPromises, mount } from '@vue/test-utils'
import { createPinia } from 'pinia'
import Permission from '@/components/Permission'

describe('Permission.vue', () => {
  it('renders the permission creation action', async () => {
    const wrapper = mount(Permission, {
      global: { plugins: [createPinia()] }
    })

    await flushPromises()
    expect(wrapper.text()).toContain('Add new permission')

    wrapper.vm.dismissCountDown = wrapper.vm.dismissSecs
    wrapper.vm.countDownChanged(4200)
    expect(wrapper.vm.countDownRemaining).toBe(4200)
    expect(wrapper.vm.dismissCountDown).toBe(wrapper.vm.dismissSecs)
  })
})
