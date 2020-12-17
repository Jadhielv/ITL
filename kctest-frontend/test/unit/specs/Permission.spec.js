import Vue from 'vue'
import Permission from '@/components/Permission'

describe('Permission.vue', () => {
  it('should render correct contents', () => {
    const Constructor = Vue.extend(Permission)
    const vm = new Constructor().$mount()
    expect(vm.$el.querySelector('.col-4 button').textContent)
      .toEqual('Add new permission')
  })
})
