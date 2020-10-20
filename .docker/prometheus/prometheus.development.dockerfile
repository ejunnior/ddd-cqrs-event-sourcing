FROM prom/prometheus
LABEL author="Edvaldo Junior"
COPY .docker/prometheus/prometheus-dev.yml /etc/prometheus/prometheus.yml
