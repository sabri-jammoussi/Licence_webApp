import type { NuxtPage } from "nuxt/schema";

export default defineNuxtConfig({
  
  devtools: { enabled: true }, 
  app: {
    head: {
      title: 'Licence-App',
      meta: [
        { charset: 'utf-8' }
      ],
      link: [
        {
          rel: 'stylesheet',
          href: 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css',
        },
      ],
      script: [
        {
          src: 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js',
          type: 'text/javascript',
          
        },
      ],
      
      
    },
  },

  css: [
    'vuetify/styles/main.sass',
    'bootstrap/dist/css/bootstrap.css',
    'primevue/resources/themes/lara-light-teal/theme.css',
    '~/assets/scss/global.scss'
  ],
 // plugins: ['@/plugins/chart.js'],

  modules: [
    '@pinia/nuxt',
    'nuxt-primevue',
    '@vee-validate/nuxt',
    '@nuxtjs/i18n',
    '@vueuse/nuxt',

    ],

    plugins: [
      { src: '~/plugins/chart.js', mode: 'client' },
      { src: '~/plugins/apex-chart.js', mode: 'client' },

      { src: '~/plugins/axios-auth.js', ssr: false }
    ],
   
    i18n: {
      vueI18n: './plugins/i18n' // if you are using custom path, default
    },
  build: {
    transpile: [
      'vuetify',
      '@fortawesome/fontawesome-svg-core',
      '@fortawesome/pro-solid-svg-icons',
      '@fortawesome/pro-regular-svg-icons',
      '@fortawesome/pro-light-svg-icons',
      '@fortawesome/free-brands-svg-icons'
    ],
    
  },

  nitro:{
    prerender: {
      crawlLinks: false,
      routes: ['/', '/register/login'],
      ignore: ['/posts/others'],
    },
  },
  // router: {
  //       // @ts-ignore
  //   extendRoutes(routes, resolve) {
  //     routes.push({
  //       name: 'AddLicence',
  //       path: '/Manager/Licences/AddLicence',
  //       component: resolve(__dirname, 'components/AddLicence.vue')
  //     });
  //   }
  // }
  
  hooks: {
    'pages:extend'(pages) {
      pages.push({
        name: 'AddLicence',
        path: '/Manager/Licences/AddLicence/:selectedApp?',
        file: '~/pages/Manager/Licences/AddLicence',
        
    
      })
    },
  },
});
