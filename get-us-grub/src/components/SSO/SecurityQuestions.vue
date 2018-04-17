<template>
                    <v-stepper-content step="2">
                    <v-form v-model="validSecurityInput">
                    <v-layout row wrap>
                      <v-flex xs12>
                        <v-select
                          :items="$store.state.constants.securityQuestionsSet1"
                          item-text="question"
                          item-value="id"
                          v-model="securityQuestions[0].question"
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
                          v-model="securityQuestions[0].answer"
                          :rules="$store.state.rules.securityAnswerRules"
                          required
                          :disabled=disable
                        ></v-text-field>
                        <v-select
                          :items="$store.state.constants.securityQuestionsSet2"
                          item-text="question"
                          item-value="id"
                          v-model="securityQuestions[1].question"
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
                        v-model="securityQuestions[1].answer"
                        :rules="$store.state.rules.securityAnswerRules"
                        required
                        :disabled=disable
                      ></v-text-field>
                      <v-select
                        :items="$store.state.constants.securityQuestionsSet3"
                        item-text="question"
                        item-value="id"
                        v-model="securityQuestions[2].question"
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
                        v-model="securityQuestions[2].answer"
                        :rules="$store.state.rules.securityAnswerRules"
                        required
                        :disabled=disable
                      ></v-text-field>
                      </v-flex>
                    </v-layout>
                    </v-form>
                    <v-btn color="grey lighten-5" @click="userStep = 1" :disabled=disable>Previous</v-btn>
                    <v-btn color="primary" @click="userSubmit" :disabled="!validSecurityInput" :loading="loading">Submit</v-btn>
                  </v-stepper-content>
</template>
