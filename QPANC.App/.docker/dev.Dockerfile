# develop stage
FROM node:12.16-alpine as develop-stage
WORKDIR /app
COPY package*.json ./
RUN npm i -g @quasar/cli@latest
COPY . .
