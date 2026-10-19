const { defineComponent, h } = require('vue')

function makeComponent (name, tag, hideWhenClosed = false) {
  return defineComponent({
    name,
    inheritAttrs: false,
    props: {
      modelValue: {
        type: [Boolean, Number],
        default: true
      }
    },
    setup (props, { attrs, slots }) {
      return () => {
        if (hideWhenClosed && !props.modelValue) return null
        return h(tag, attrs, slots.default ? slots.default() : undefined)
      }
    }
  })
}

exports.BAlert = makeComponent('BAlert', 'div', true)
exports.BApp = makeComponent('BApp', 'div')
exports.BButton = makeComponent('BButton', 'button')
exports.BForm = makeComponent('BForm', 'form')
exports.BFormGroup = makeComponent('BFormGroup', 'div')
exports.BFormInput = makeComponent('BFormInput', 'input')
exports.BFormSelect = makeComponent('BFormSelect', 'select')
exports.BModal = makeComponent('BModal', 'div', true)
exports.BProgress = makeComponent('BProgress', 'div')
