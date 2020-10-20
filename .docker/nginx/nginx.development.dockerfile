FROM nginx:alpine
LABEL author="Edvaldo Junior"
EXPOSE 80
RUN rm /etc/nginx/nginx.conf
COPY .docker/nginx/nginx-dev.conf /etc/nginx/nginx.conf