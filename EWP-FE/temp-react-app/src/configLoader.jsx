import yaml from 'js-yaml'
import fs from 'fs'


export const loadConfig = () => {
  try {
    const env = import.meta.env.REACT_APP_ENV || 'development'
    const fileContents = fs.readFileSync('../config.yaml', 'utf8')
    const data = yaml.load(fileContents)

    if (data.environments[env]) {
      return data.environments[env]
    } else {
      throw new Error(`Environment ${env} not found in configuration.`)
    }
  } catch (e) {
    console.error('Error loading configuration:', e)
    return null
  }
}