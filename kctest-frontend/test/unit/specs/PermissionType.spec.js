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
  })
})
