import * as functions from 'firebase-functions';
import * as fs from "fs";

const REGION = 'asia-northeast1'
// TODO イベントタイプを定数として定義するのではなく、ディレクトリ名から動的に取得する
const EVENT_TYPES = ['Joy', 'Sadness', 'System']

export const getRandomEvent = functions
  .region(REGION)
  .https.onRequest(async (request, response) => {
    const eventType = request.query['eventType'] as string
    console.log(`eventType = ${eventType}`)

    if (!eventType || !EVENT_TYPES.includes(eventType)) {
      console.log('eventType is invalid.')
      response.status(400)
      response.send('Bad Request')
      return
    }

    const eventTypeDir = `./Events/${eventType}/`
    console.log(`eventTypeDir = ${eventTypeDir}`)
    const files = fs.readdirSync(eventTypeDir)
    console.log(`files = ${files}`)
    const scriptFiles = files.filter( f => f.endsWith('.script'))
    console.log(`scriptFiles = ${scriptFiles}`)
    const randomFile = scriptFiles[Math.floor(Math.random() * scriptFiles.length)]
    console.log(`randomFile = ${randomFile}`)
    const content = fs.readFileSync(eventTypeDir + randomFile)

    response.set('Access-Control-Allow-Origin', '*')
    response.set('Access-Control-Allow-Methods', 'GET')
    response.set('Access-Control-Allow-Headers', 'Content-Type')
    // https://stackoverflow.com/questions/5822985/cross-domain-resource-sharing-get-refused-to-get-unsafe-header-etag-from-re
    response.set('Access-Control-Expose-Headers', 'ETag')
    response.contentType('text/plain')
    response.send(content);
  });

export const getEvent = functions
  .region(REGION)
  .https.onRequest(async (request, response) => {
    const eventType = request.query['eventType'] as string
    const eventName = request.query['eventName'] as string
    console.log(`eventType = ${eventType}, eventName = ${eventName}`)

    // イベント名が '..' を含まないようにしているのは、ディレクトリトラバーサル対策
    if (!eventType || !EVENT_TYPES.includes(eventType) || !eventName || eventName.includes('..')) {
      console.log('eventType or eventName is invalid.')
      response.status(400)
      response.send('Bad Request')
      return
    }

    const content = fs.readFileSync(`./Events/${eventType}/${eventName}.script`)

    response.set('Access-Control-Allow-Origin', '*')
    response.set('Access-Control-Allow-Methods', 'GET')
    response.set('Access-Control-Allow-Headers', 'Content-Type')
    // https://stackoverflow.com/questions/5822985/cross-domain-resource-sharing-get-refused-to-get-unsafe-header-etag-from-re
    response.set('Access-Control-Expose-Headers', 'ETag')
    response.contentType('text/plain')
    response.send(content);
  });
