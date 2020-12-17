import Vue from 'vue'
import PermissionType from '@/components/PermissionType'

describe('PermissionType.vue', () => {
  it('should render correct contents', () => {
    const Constructor = Vue.extend(PermissionType)
    const vm = new Constructor().$mount()
    expect(vm.$el.querySelector('.col-4 button').textContent)
      .toEqual('Add new permission type')
  })
})
