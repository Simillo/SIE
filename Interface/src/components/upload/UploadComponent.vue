<template lang='pug'>
  .uploads
    md-field
      label Upload de arquivos
      md-file(v-model="uploads" multiple="" name="files" @change="fnUpload($event.target.files)")
    .preview
      .preview-uploads(v-for="(item, index) in preview" :key="index")
        img.preview-image(:src="item")

</template>

<script>
export default {
  props: ['data'],
  data () {
    return {
      uploads: '',
      preview: [],
      form: new FormData()
    }
  },
  methods: {
    fnUpload (files) {
      console.log(files)
      this.preview.push(...files)
    }
  }
}
</script>

<style scoped lang='scss'>
.preview {
  display: inline-flex;
  flex-wrap: wrap;
  justify-content: center;
  margin-bottom: 20px;
  .preview-uploads {
    margin: 10px;
    width: 100px;
    .preview-image {
      width: 250px;
    }
  }
}
</style>
