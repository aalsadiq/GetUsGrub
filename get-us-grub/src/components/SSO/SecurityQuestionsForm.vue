<template>
  <div>
    <div v-for="set in $store.state.constants.securityQuestions" :key="set.id">
      <v-select
        :items="set.questions"
        item-text="question"
        item-value="id"
        v-model="securityQuestions[set.id].question"
        label="Select a security question"
        single-line
        auto
        append-icon="https"
        hide-details
        :rules="$store.state.rules.securityQuestionRules"
        required
        :disabled=disable
        ></v-select>
      <v-text-field
        label="Enter an answer to the above security question"
        v-model="securityQuestions[set.id].answer"
        :rules="$store.state.rules.securityAnswerRules"
        required
        :disabled=disable
        ></v-text-field>
    </div>
  </div>
</template>

<script>
export default {
  name: 'SecurityQuestionsForm',
  components: { },
  props: [
    'securityQuestions'
  ]
}
</script>
