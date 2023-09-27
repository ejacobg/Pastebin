# ==================================================================================== #
# DOCKER
# ==================================================================================== #

.PHONY: docker/build
docker/build:
	make -C ./Shortener docker/build
	make -C ./Lengthener docker/build

# ==================================================================================== #
# KUBERNETES
# ==================================================================================== #

.PHONY: k8s/pastebin/apply
k8s/pastebin/apply:
	kubectl apply -f ./Shortener/shortener.yaml
	kubectl apply -f ./Lengthener/lengthener.yaml
	kubectl apply -f ./pastebin.yaml

.PHONY: k8s/pastebin/check
k8s/pastebin/check:
	kubectl get pods
	kubectl describe ingress pastebin

.PHONY: k8s/pastebin/delete
k8s/pastebin/delete:
	kubectl delete -f ./Shortener/shortener.yaml
	kubectl delete -f ./Lengthener/lengthener.yaml
	kubectl delete -f ./pastebin.yaml

.PHONY: k8s/ingress/apply
k8s/ingress/apply:
	kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml

.PHONY: k8s/ingress/check
k8s/ingress/check:
	kubectl get all -n ingress-nginx

.PHONY: k8s/ingress/delete
k8s/ingress/delete:
	kubectl delete namespace ingress-nginx
#	kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml

.PHONY: k8s/apply
k8s/apply: k8s/ingress/apply k8s/pastebin/apply

.PHONY: k8s/delete
k8s/delete: k8s/pastebin/delete k8s/ingress/delete
