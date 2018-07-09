<template lang='pug'>
  .uploads
    span.title {{title}}
    div(v-if='enabled')
      md-field(v-if='multiple')
        md-file(
          multiple,
          name='files',
          @change='fnUpload($event.target.files)'
        )
      md-field(v-else)
        md-file(
          name='files',
          @change='fnUpload($event.target.files)'
        )
    .file-list
      .file-list-item(
        v-for='(file, index) in fileList',
        :key='index'
      )
        div(v-if='!preview')
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
        div(
          v-else,
          @click='deleteFile(index)'
        )
          md-avatar
            md-tooltip(md-direction='right') Excluir
            img(
              v-if='!multiple',
              :src='file'
              )

</template>

<script>
import UploadService from '../../services/UploadService.js'
export default {
  props: ['fileName', 'canUpload', 'files', 'title', 'multiple', 'preview'],
  data () {
    return {
      fileList: [],
      form: new FormData(),
      enabled: this.canUpload
    }
  },
  created () {
    this.fileList = this.files
    this.service = new UploadService(this.$http)
    if (!this.multiple && this.fileList.length) this.enabled = false
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
      if (!this.multiple) this.enabled = false
    },
    generateName (file, index) {
      const extension = file.match(/\.(.*)$/gi)
      if (this.multiple) return `${this.fileName}_${index + 1}${extension}`
      return `${this.fileName}${extension}`
    },
    deleteFile (index) {
      this.fileList.splice(index, 1)
      if (!this.multiple) this.enabled = true
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
  .title {
    text-align: center;
    display: block;
    font-size: 15px;
  }
}
.file-list {
  text-align: center;
  margin-bottom: 20px;
  .file-list-item {
    border-radius: .2em;
    padding: 5px 0 5px;
    border: 1px solid #999;
    margin: 10px;
    .img-preview {
      width: 100px;
      height: 100px;
      border-radius: 100%;
    }
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
.md-avatar {
  cursor: pointer;
  width: 100px;
  height: 100px;
  border-radius: 100%;
}
</style>
