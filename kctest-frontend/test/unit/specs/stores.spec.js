import axios from 'axios'
import { createPinia, setActivePinia } from 'pinia'
import { usePermissionTypesStore } from '@/stores/permissionTypes'
import { usePermissionsStore } from '@/stores/permissions'

describe('Pinia stores', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
    jest.clearAllMocks()
  })

  it('loads permissions and normalizes their dates', async () => {
    axios.get.mockResolvedValueOnce({
      data: [{
        id: 1,
        date: '2024-04-03T00:00:00',
        permissionType: { id: 2, description: 'Vacation' }
      }]
    })
    const store = usePermissionsStore()

    await expect(store.load()).resolves.toBe(true)
    expect(store.items[0].date).toBe('2024-04-03')
  })

  it('updates permission state only after successful API requests', async () => {
    const store = usePermissionsStore()
    const permission = { id: 1, name: 'Ada', lastName: 'Lovelace', date: '2024-04-03' }
    axios.post.mockResolvedValueOnce({ data: permission })
    axios.put.mockResolvedValueOnce({ data: '' })
    axios.delete.mockResolvedValueOnce({ data: '' })

    await expect(store.create(permission)).resolves.toBe(true)
    await expect(store.update({ ...permission, name: 'Augusta' })).resolves.toBe(true)
    expect(store.items[0].name).toBe('Augusta')
    await expect(store.remove(1)).resolves.toBe(true)
    expect(store.items).toEqual([])
  })

  it('supports permission type create, update, and delete actions', async () => {
    const store = usePermissionTypesStore()
    const permissionType = { id: 2, description: 'Sick leave' }
    axios.post.mockResolvedValueOnce({ data: permissionType })
    axios.put.mockResolvedValueOnce({ data: '' })
    axios.delete.mockResolvedValueOnce({ data: '' })

    await expect(store.create(permissionType)).resolves.toBe(true)
    await expect(store.update({ ...permissionType, description: 'Vacation' })).resolves.toBe(true)
    expect(store.items[0].description).toBe('Vacation')
    await expect(store.remove(2)).resolves.toBe(true)
    expect(store.items).toEqual([])
  })

  it('surfaces API failures in store state', async () => {
    const store = usePermissionTypesStore()
    axios.get.mockRejectedValueOnce(new Error('offline'))

    await expect(store.load()).resolves.toBe(false)
    expect(store.error).toContain('offline')
  })
})
