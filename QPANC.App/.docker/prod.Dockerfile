# develop stage
FROM node:12.16-alpine as develop-stage
WORKDIR /app/source
COPY package*.json ./
RUN npm i -g @quasar/cli@latest
COPY . .

# local-deps
FROM develop-stage as local-deps-stage
RUN ls
RUN rm -r -f .quasar
RUN rm -r -f dist
RUN rm -r -f node_moduless
RUN rm -f yarn.lock
RUN yarn

# build stage
FROM local-deps-stage as build-stage
ENV API_CLIENT_URL=https://api.qpanc.tobiasmesquita.dev/
ENV API_SERVER_URL=https://api.qpanc.tobiasmesquita.dev/
RUN quasar build -m ssr

# prod stage
FROM build-stage as prod-stage
WORKDIR /app/source/dist/ssr
RUN mv * ../../../
WORKDIR /app
RUN rm -r -f source
RUN yarn
CMD ["ls"]
CMD ["node", "index.js"]
