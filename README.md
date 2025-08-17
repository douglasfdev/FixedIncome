# Essa aplicação é um caso de uso para processar arquivo CSV com mais de 1 milhão de registros em menos de 20 minutos.

## Foi utilizado:
### Channels
### SemaphreSlim
### MongoDB

Foi feito os testes dentro de um container com 0.5 de CPU e 512MB de RAM
<img width="510" height="156" alt="Captura de tela 2025-08-17 033909" src="https://github.com/user-attachments/assets/4ec26ec0-b38a-4a07-b0a8-33cb8db344ff" />

Processo termina antes de 20 minutos.
