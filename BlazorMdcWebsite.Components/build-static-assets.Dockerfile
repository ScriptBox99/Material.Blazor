FROM node
WORKDIR /wip
COPY package.json .
RUN npm install
COPY . .
RUN npm run build-mt-default
RUN npm run minify-mt-default
RUN npm run build-blue-square
RUN npm run minify-blue-square
RUN npm run build-red-round
RUN npm run minify-red-round
RUN npm run build-varied
RUN npm run minify-varied
WORKDIR /wip/wwwroot/css