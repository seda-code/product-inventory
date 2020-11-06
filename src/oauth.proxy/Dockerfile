FROM node:lts-alpine

WORKDIR /app

COPY . .

RUN apk --no-cache --virtual build-dependencies add \
    python \
    make \
    g++ \
    && npm install \
    && apk del build-dependencies

EXPOSE 3000

CMD ["node", "proxy.js"]