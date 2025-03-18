import axios from 'axios'

const baseUrl = 'https://localhost:7005/api/v0'

export default axios.create({
  baseURL: baseUrl,
  headers: {
    'Content-Type': 'application/json',
  },
})
