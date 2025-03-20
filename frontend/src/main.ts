import './assets/main.css'
import 'primeicons/primeicons.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import Lara from '@primeuix/themes/lara'

// Primevue
import PrimeVue from 'primevue/config'

// Primevue Components
import { Button, Fieldset, FloatLabel, MultiSelect, Select, Textarea, ToggleButton } from 'primevue'
import Tabs from 'primevue/tabs'
import TabList from 'primevue/tablist'
import Tab from 'primevue/tab'
import TabPanels from 'primevue/tabpanels'
import TabPanel from 'primevue/tabpanel'
import DynamicDialog from 'primevue/dynamicdialog'
import InputText from 'primevue/inputtext'
import Toast from 'primevue/toast'
import ConfirmDialog from 'primevue/confirmdialog'
import ConfirmPopup from 'primevue/confirmpopup'
import { Form } from '@primevue/forms'

// Services
import ToastService from 'primevue/toastservice'
import DialogService from 'primevue/dialogservice'
import ConfirmationService from 'primevue/confirmationservice'

import { definePreset } from '@primeuix/themes'

const app = createApp(App)

// Component use
app.component('pv-button', Button)
app.component('pv-tabs', Tabs)
app.component('pv-tab', Tab)
app.component('pv-tab-panels', TabPanels)
app.component('pv-tab-list', TabList)
app.component('pv-tab-panel', TabPanel)
app.component('pv-dynamic-dialog', DynamicDialog)
app.component('pv-input-text', InputText)
app.component('pv-text-area', Textarea)
app.component('pv-float-label', FloatLabel)
app.component('pv-fieldset', Fieldset)
app.component('pv-multi-select', MultiSelect)
app.component('pv-toast', Toast)
app.component('pv-confirm-dialog', ConfirmDialog)
app.component('pv-form', Form)
app.component('pv-toggle-button', ToggleButton)
app.component('pv-confirm-popup', ConfirmPopup)
app.component("pv-select", Select)

// Enable Ripple and use Primevue

// Noir Preset
const Noir = definePreset(Lara, {
  semantic: {
    primary: {
      50: '{zinc.50}',
      100: '{zinc.100}',
      200: '{zinc.200}',
      300: '{zinc.300}',
      400: '{zinc.400}',
      500: '{zinc.500}',
      600: '{zinc.600}',
      700: '{zinc.700}',
      800: '{zinc.800}',
      900: '{zinc.900}',
      950: '{zinc.950}',
    },
    colorScheme: {
      light: {
        primary: {
          color: '{zinc.950}',
          inverseColor: '#ffffff',
          hoverColor: '{zinc.900}',
          activeColor: '{zinc.800}',
        },
        highlight: {
          background: '{zinc.950}',
          focusBackground: '{zinc.700}',
          color: '#ffffff',
          focusColor: '#ffffff',
        },
      },
      dark: {
        primary: {
          color: '{zinc.50}',
          inverseColor: '{zinc.950}',
          hoverColor: '{zinc.100}',
          activeColor: '{zinc.200}',
        },
        highlight: {
          background: 'rgba(250, 250, 250, .16)',
          focusBackground: 'rgba(250, 250, 250, .24)',
          color: 'rgba(255,255,255,.87)',
          focusColor: 'rgba(255,255,255,.87)',
        },
      },
    },
  },
})

app.use(PrimeVue, {
  ripple: true,
  theme: {
    preset: Noir,
    options: {
      prefix: 'p',
      darkModeSelector: '.p-dark',
      cssLayer: false,
    },
  },
})

const root = document.getElementsByTagName('html')[0]
root.classList.toggle('p-dark')
app.use(ConfirmationService)
app.use(DialogService)
app.use(ToastService)
app.use(createPinia())
app.use(router)

app.mount('#app')
