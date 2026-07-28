(function () {
    'use strict'; angular.module('omnitrixApp', []).controller('AlienController', AlienController);

    AlienController.$inject = ['$http'];

    function AlienController($http) {
        var vm = this;

        vm.aliens = [];
        vm.especiesDisponiveis = [];
        vm.busca = '';
        vm.especieSelecionada = '';
        vm.forcaMinima = null;
        vm.carregando = true;
        vm.erro = '';

        vm.filtrarAliens = filtrarAliens;
        vm.limparFiltros = limparFiltros;
        vm.obterImagemAlien = obterImagemAlien;
        vm.gerarDescricao = gerarDescricao;
        vm.obterNome = obterNome;
        vm.obterEspecie = obterEspecie;
        vm.obterPlaneta = obterPlaneta;
        vm.obterForca = obterForca;

        var urlApi = 'https://gist.githubusercontent.com/edvandosimplicio/88943f13c2effed750ed3081deca4ab5/raw/8ed2da1c1b6dacd6d0e09efca6ab3a449b0a37cd/apiv2-ben10-aliens.json';

        carregarAliens();

        function carregarAliens() {
            vm.carregando = true;
            vm.erro = '';

            $http.get(urlApi)
                .then(function (response) {
                    vm.aliens = response.data || [];
                    montarListaDeEspecies();

                    console.log('Aliens carregados:', vm.aliens);
                })
                .catch(function (error) {
                    console.error('Erro ao carregar aliens:', error);
                    vm.erro = 'Não foi possível carregar os aliens. Verifique sua conexão ou tente novamente.';
                })
                .finally(function () {
                    vm.carregando = false;
                });
        }

        function montarListaDeEspecies() {
            var especies = {};

            vm.aliens.forEach(function (alien) {
                var especie = obterEspecie(alien);

                if (especie) {
                    especies[especie] = true;
                }
            });

            vm.especiesDisponiveis = Object.keys(especies).sort();
        }

        function filtrarAliens(alien) {
            var textoBusca = normalizarTexto(vm.busca);

            var nome = normalizarTexto(obterNome(alien));
            var especie = normalizarTexto(obterEspecie(alien));
            var planeta = normalizarTexto(obterPlaneta(alien));

            var passouNaBusca = true;

            if (textoBusca) {
                passouNaBusca =
                    nome.includes(textoBusca) ||
                    especie.includes(textoBusca) ||
                    planeta.includes(textoBusca);
            }

            var passouNaEspecie = true;

            if (vm.especieSelecionada) {
                passouNaEspecie = obterEspecie(alien) === vm.especieSelecionada;
            }

            var passouNaForca = true;

            if (vm.forcaMinima) {
                passouNaForca = obterForca(alien) >= vm.forcaMinima;
            }

            return passouNaBusca && passouNaEspecie && passouNaForca;
        }

        function limparFiltros() {
            vm.busca = '';
            vm.especieSelecionada = '';
            vm.forcaMinima = null;
        }

        function obterImagemAlien(alien) {
            return alien.image || 'assets/images/placeholder.png';
        }

        function gerarDescricao(alien) {
            if (alien.description) {
                return alien.description;
            }

            var nome = obterNome(alien);
            var especie = obterEspecie(alien);
            var planeta = obterPlaneta(alien);
            var forca = obterForca(alien);

            var classificacao = '';

            if (forca >= 90) {
                classificacao = 'É uma das transformações mais poderosas disponíveis no Omnitrix.';
            } else if (forca >= 70) {
                classificacao = 'Possui ótimo equilíbrio entre força, resistência e versatilidade.';
            } else if (forca >= 50) {
                classificacao = 'É uma transformação útil para situações estratégicas.';
            } else {
                classificacao = 'Apesar de sua força menor, pode ser decisivo quando usado com inteligência.';
            }

            return nome + ' é um alien da espécie ' + especie +
                ', originário de ' + planeta +
                '. Sua força base é ' + forca + '. ' +
                classificacao;
        }

        function obterNome(alien) {
            return alien.name || alien.nome || 'Alien desconhecido';
        }

        function obterEspecie(alien) {
            return alien.species || alien.especie || 'Espécie desconhecida';
        }

        function obterPlaneta(alien) {
            return alien.homeworld
                || alien.planetaOrigem
                || alien.planet
                || alien.planeta
                || 'Desconhecido';
        }

        function obterForca(alien) {
            return alien.strength
                || alien.forcaBase
                || alien.forca
                || 85;
        }

        function normalizarTexto(texto) {
            return (texto || '')
                .toString()
                .toLowerCase()
                .normalize('NFD')
                .replace(/[\u0300-\u036f]/g, '');
        }
    }
})();