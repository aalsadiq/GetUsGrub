<template>
  <v-form v-model="value.isValid" v-on:input="$emit('input', value)">
    <!-- CREATE A SECURITY QUESTION & ANSWER FIELD FOR EACH SET IN STORE -->
    <div v-for="set in $store.state.constants.securityQuestions" :key="set.id">
      <v-select
        :items="set.questions"
        item-text="question"
        item-value="id"
        v-model="value.questions[set.id].question"
        label="Select a security question"
        single-line
        auto
        append-icon="https"
        hide-details
        :rules="$store.state.rules.securityQuestionRules"
        required
        :disabled="disabled"
        ></v-select>
      <v-text-field
        label="Enter an answer to the above security question"
        v-model="value.questions[set.id].answer"
        :rules="$store.state.rules.securityAnswerRules"
        required
        :disabled="disabled"
        ></v-text-field>
      </div>
  </v-form>
</template>

<script>
export default {
  name: 'security-questions',
  components: { },
  props: [
    'value',
    'disabled'
  ]
}
</script>
