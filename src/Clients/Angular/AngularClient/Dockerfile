FROM node:14.15.3 AS node
WORKDIR /Angular/AngularClient
COPY . .

RUN npm install -g @angular/cli && \
    npm install && \
    ng build

FROM nginx:alpine
COPY --from=node /Angular/AngularClient/dist/AngularClient /usr/share/nginx/html