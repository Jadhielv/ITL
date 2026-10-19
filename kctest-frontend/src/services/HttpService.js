import axios from 'axios'

export default class HttpService {
  baseUrl = ''

  constructor () {
    this.baseUrl = '/api'
  }

  async get (endpoint = '') {
    return (await axios.get(`${this.baseUrl}/${endpoint}`)).data
  }

  async getById (endpoint = '', id = '') {
    return (await axios.get(`${this.baseUrl}/${endpoint}/${id}`)).data
  }

  async post (endpoint = '', body) {
    return (await axios.post(`${this.baseUrl}/${endpoint}`, body)).data
  }

  async put (endpoint = '', body) {
    return (await axios.put(`${this.baseUrl}/${endpoint}`, body)).data
  }

  async delete (endpoint = '', id) {
    return (await axios.delete(`${this.baseUrl}/${endpoint}/${id}`)).data
  }
}
