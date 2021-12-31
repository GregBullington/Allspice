import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async edit(account) {
    const res = await api.put('/account', account)
    AppState.account = res.data
    logger.log('post', res.data.xp)
  }
}

export const accountService = new AccountService()
