(function () {
    'use strict'; angular.module('omnitrixApp', []).controller('AlienController', AlienController);

    AlienController.$inject = ['$scope', '$http'];

    function AlienController($scope, $http) {

        $scope.aliens = [];
        $scope.especiesDisponiveis = [];
        $scope.busca = '';
        $scope.especieSelecionada = '';
        $scope.forcaMinima = null;
        $scope.carregando = true;
        $scope.erro = '';

        $scope.filtrarAliens = filtrarAliens;
        $scope.limparFiltros = limparFiltros;
        $scope.obterImagemAlien = obterImagemAlien;
        $scope.gerarDescricao = gerarDescricao;
        $scope.obterNome = obterNome;
        $scope.obterEspecie = obterEspecie;
        $scope.obterPlaneta = obterPlaneta;
        $scope.obterForca = obterForca;

        var urlApi = 'https://gist.githubusercontent.com/edvandosimplicio/88943f13c2effed750ed3081deca4ab5/raw/53a8b0c16344eed3acb0f3bcfaae7026d559ad7d/apiv2-ben10-aliens.json';

        carregarAliens();

        function carregarAliens() {
            $scope.carregando = true;
            $scope.erro = '';

            $http.get(urlApi)
                .then(function (response) {
                    $scope.aliens = response.data || [];
                    montarListaDeEspecies();

                    console.log('Aliens carregados:', $scope.aliens);
                })
                .catch(function (error) {
                    console.error('Erro ao carregar aliens:', error);
                    $scope.erro = 'Não foi possível carregar os aliens. Verifique sua conexão ou tente novamente.';
                })
                .finally(function () {
                    $scope.carregando = false;
                });
        }

        function montarListaDeEspecies() {
            var especies = {};

            $scope.aliens.forEach(function (alien) {
                var especie = obterEspecie(alien);

                if (especie) {
                    especies[especie] = true;
                }
            });

            $scope.especiesDisponiveis = Object.keys(especies).sort();
        }

        function filtrarAliens(alien) {
            var textoBusca = normalizarTexto($scope.busca);

            var nome = normalizarTexto(obterNome(alien));
            var especie = normalizarTexto(obterEspecie(alien));
            var planeta = normalizarTexto(obterPlaneta(alien));

            var passouNaBusca = true;

            if (textoBusca) {
                passouNaBusca = nome.includes(textoBusca) || especie.includes(textoBusca) || planeta.includes(textoBusca);
            }

            var passouNaEspecie = true;

            if ($scope.especieSelecionada) {
                passouNaEspecie = obterEspecie(alien) === $scope.especieSelecionada;
            }

            var passouNaForca = true;

            if ($scope.forcaMinima) {
                passouNaForca = obterForca(alien) >= $scope.forcaMinima;
            }

            return passouNaBusca && passouNaEspecie && passouNaForca;
        }

        function limparFiltros() {
            $scope.busca = '';
            $scope.especieSelecionada = '';
            $scope.forcaMinima = null;
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
            return alien.homeworld || alien.planetaOrigem || 'Desconhecido';
        }

        function obterForca(alien) {
            return alien.strength || alien.forcaBase || alien.forca || 85;
        }

        function normalizarTexto(texto) {
            return (texto || '').toString().toLowerCase().normalize('NFD').replace(/[\u0300-\u036f]/g, '');
        }
    }
})();