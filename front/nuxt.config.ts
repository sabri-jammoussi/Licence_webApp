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
    'primevue/resources/themes/aura-light-green/theme.css',
  ],


  modules: [
    '@pinia/nuxt',
    'nuxt-primevue',
    '@vee-validate/nuxt',
    ],

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
 

});
