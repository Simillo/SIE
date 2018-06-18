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
        span.file-list-item-icon(
          @click='deleteFile(index)',
          v-if='canUpload'
        )
          md-tooltip.margin-tooltip Excluir
          md-icon close

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
    },
    deleteFile (index) {
      this.fileList.splice(index, 1)
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
  padding: 10px 10px 0px;
}
.file-list {
  text-align: center;
  margin-bottom: 20px;
  .file-list-item {
    border-radius: .2em;
    padding: 5px 0 5px;
    border: 1px solid #999;
    margin: 10px;
    .file-list-item-icon {
      cursor: pointer;
      margin-left: 10px;
      .md-icon {
        color: #f00;
        a {
          text-decoration: none;
        }
      }
    }
  }
}
</style>
