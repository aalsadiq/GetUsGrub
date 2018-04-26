<script>
import { Pie, mixins } from 'vue-chartjs'
import { EventBus } from '@/event-bus/event-bus.js'
const { reactiveProp } = mixins

export default {
  extends: Pie,
  mixins: [reactiveProp],
  props: ['options', 'billItem', 'billItemIndex'],
  data () {
    return {
      // billUsers: this.billItem.selected,
      // labelList: [],
      // labels: this.billItem.selected,
      datacollection: null,            
    }
  },
  mounted () {
    EventBus.$on('users-in-bill-item', payload => {
      this.updatePieChartLabels(payload)
    })
    this.fillData()
    this.renderChart(this.datacollection, { responsive: true, maintainAspectRatio: false })
  },
  updated () {
  },
  methods: {
    fillData: function () {
      this.datacollection = {
        datasets: [
          {
            label: 'Remaining',
            backgroundColor: '#f87979',
            data: [this.billItem.itemPrice]
          },
          {
            label: 'Test1',
            backgroundColor: '#ffffff',
            data: [100]
          },
          {
            label: 'Test2',
            backgroundColor: '#00ff00',
            data: [10]
          }
        ]
      }
    },
    updatePieChartLabels: function (payload) {
      // for (var i = 1; i < this.labels.length; i++) {
      //   this.labels[i].pop()
      // }
      // for (var j = 1; j < payload.selected.length; j++) {
      //   this.labels[j] = payload.selected[j]
      // }
    }
  },
  computed: {
    remainingItemPrice () {
      return 0
    },
    billItems () {
      return this.$store.state.billItems
    }
  }
}
</script>

<style scoped>

</style>
