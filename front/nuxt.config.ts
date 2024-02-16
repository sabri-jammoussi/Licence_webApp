export default defineNuxtConfig({
  components: true,

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
  devtools: { enabled: true },

  css: [
    'vuetify/styles/main.sass',
    'bootstrap/dist/css/bootstrap.css',
  ],

  build: {
    transpile: ['vuetify'],
  },

  // Other configurations...
});
