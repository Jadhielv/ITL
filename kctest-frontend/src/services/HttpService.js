import axios from 'axios'

export default class HttpService {
  baseUrl = ''

  constructor () {
    this.baseUrl = 'https://localhost:5001/api'
  }

  async get (endpoint = '') {
    try {
      return await (await axios.get(`${this.baseUrl}/${endpoint}`)).data
    } catch (error) {
      console.log(error)
    }
  }

  async getById (endpoint = '', id = '') {
    try {
      return await (await axios.get(`${this.baseUrl}/${endpoint}/${id}`)).data
    } catch (error) {
      console.log(error)
    }
  }

  async post (endpoint = '', body) {
    try {
      return await (await axios.post(`${this.baseUrl}/${endpoint}`, body)).data
    } catch (error) {
      console.log(error)
    }
  }

  async put (endpoint = '', body) {
    try {
      return await (await axios.put(`${this.baseUrl}/${endpoint}`, body)).data
    } catch (error) {
      console.log(error)
    }
  }

  async delete (endpoint = '', id) {
    try {
      return await (await axios.delete(`${this.baseUrl}/${endpoint}/${id}`)).data
    } catch (error) {
      console.log(error)
    }
  }
}
