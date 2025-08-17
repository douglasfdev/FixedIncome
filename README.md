# Essa aplicação é um caso de uso para processar arquivo CSV com mais de 1 milhão de registros em menos de 20 minutos.

## Foi utilizado:
### Channels
### SemaphoreSlim
### MongoDB

Foi feito os testes dentro de um container com 0.5 de CPU e 512MB de RAM
<img width="514" height="179" alt="image" src="https://github.com/user-attachments/assets/b7954640-d832-4c06-8665-91be67bf4482" />


Processo termina antes de 30 segundos.
