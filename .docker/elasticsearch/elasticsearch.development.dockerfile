FROM docker.elastic.co/elasticsearch/elasticsearch:7.8.0
LABEL author="Edvaldo Junior"
COPY .docker/elasticsearch/elasticsearch.yml /usr/share/elasticsearch/config/elasticsearch.yml
EXPOSE 9200 9300