import en from '../locales/en.json'
import fr from '../locales/fr.json'

export default defineI18nConfig(() => ({
    legacy: false,
    locale: 'en',
    locales: [
      { code: 'en', iso: 'en-US', name: "English(US)", file: 'en.json' },
      { code: 'fr', iso: 'fr-FR', name :"Français",file: 'fr.json' }
    ],
    defaultLocale: 'fr',

      fallbackLocale: 'en',
      messages: {
        en,
        fr
      }
    
  }))
  