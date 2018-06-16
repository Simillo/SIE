<template lang='pug'>
  .uploads
    md-field(v-if='canUpload')
      label Upload de arquivos
      md-file(
        multiple,
        name='files',
        @change='fnUpload($event.target.files)'
      )
    .file-list
      .file-list-item(
        v-for='(file, index) in fileList',
        :key='index'
      )
        a.file-list-item-link(
          :href='file',
          target='_blank'
        ) {{generateName(file, index)}}
</template>

<script>
import UploadService from '../../services/UploadService.js'
export default {
  props: ['fileName', 'canUpload', 'files'],
  data () {
    return {
      fileList: [],
      form: new FormData()
    }
  },
  created () {
    this.fileList = this.files
    this.service = new UploadService(this.$http)
  },
  methods: {
    async fnUpload (files) {
      if (!this.canUpload) return

      const filesForm = new FormData()
      for (let i = 0; i < files.length; ++i) {
        filesForm.append(`file_${i}`, files[i], files[i].name)
      }
      const response = await this.service.upload(filesForm)
      this.fileList = response.data.entity
      this.$emit('update:files', this.fileList)
    },
    generateName (file, index) {
      const extension = file.match(/\.(.*)$/gi)
      return `${this.fileName}_${index + 1}${extension}`
    }
  }
}
</script>

<style scoped lang='scss'>
.uploads {
  width: 80%;
  margin: 0 auto;
  margin-bottom: 20px;
  border: 1px solid #999;
  border-radius: .2em;
  padding: 10px;
}
.file-list {
  text-align: center;
  margin-bottom: 20px;
  .file-list-item {
    margin: 10px;
  }
}
</style>
