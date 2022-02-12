# V8 SRL Developer Test


## Para generar la base de datos
  * Crear una base de datos en SQL Server llamada "MarshallsLLC" (Se puede cambiar el nombre, pero se debe modificar el string de conexion en el appsettings del proyecto API)
  * Colocar el proyecto Salaries.Infrastructure como proyecto de Inicio
  * Abrimos la consola de nuget
  * Ejecutamos el comando "Add-Migration InitialCreate"
  * Ejecutamos el comando Update-Database