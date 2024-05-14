# Use the official Node.js image as a base
FROM node:latest

# Set the working directory in the container
RUN mkdir -p /user/src/nuxt-app
WORKDIR /user/src/nuxt-app

VOLUME /app/assets
VOLUME /app/components
VOLUME /app/pages

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Remove package-lock.json and node_modules directory
RUN rm -rf package-lock.json node_modules
RUN npm cache clean --force
RUN npm install --force
RUN npm run build --force
# Copy all files from the current directory to the working directory

ENV NODE_NO_WARNINGS=1
COPY . .

# Expose the port that Nuxt.js is running on (default is 3000)
EXPOSE 3000

# Start the Nuxt.js application

CMD ["npm", "run", "dev"]


