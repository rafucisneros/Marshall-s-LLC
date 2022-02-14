# V8 SRL Developer Test

## Instrucciones de ejecución

EL proyecto se ejecuta usando Visual Studio 2019

### Para generar la base de datos
  * Crear una base de datos en SQL Server llamada "MarshallsLLC" (Se puede cambiar el nombre, pero se debe modificar el string de conexion en el appsettings del proyecto API)
  * Colocar el proyecto Salaries.Infrastructure como proyecto de Inicio
  * Abrimos la consola de nuget
  * Seleccionamos en la consola el proyecto "Salaries.Infrastructure" como proyecto por default
  * Ejecutamos el comando "Add-Migration InitialCreate"
  * Ejecutamos el comando Update-Database
  
### Para ejecutar la solución
  * Abrimos las propiedades de la solución
  * Cambiamos la configuracion de arranque para iniciar multiples proyectos en simultaneo
  * Cambiamos la configuración de los proyectos "Salaries.AngularFront", "Salaries.API" y "Salaries.ASPFront", colocandoles la acción de Iniciar
  * Cerramos la ventana de propiedades y se ejecuta la solucion con el boton "Start" en el menu superior de VS