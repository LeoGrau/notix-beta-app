# Use Node.js image to build the Vue app
FROM node:18 AS build
WORKDIR /app

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install
COPY . .

# Build the Vue.js app
RUN npm run build

# Use Node.js to serve the built app
FROM node:18 AS serve
WORKDIR /app

# Install serve globally in Notix! 
RUN npm install -g serve

# Copy built files from previous stage
COPY --from=build /app/dist /app/dist

# Expose the port
EXPOSE 80

# Start the server
CMD ["serve", "-s", "dist", "-l", "80"]
