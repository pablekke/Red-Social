# Red-Social-Parte-1
Aplicación de redes sociales, parte de solo backed.
-----------------LETRA DE LA APLICACIÓN--------------------------------

Una empresa de medios nos ha encomendado la tarea de crear una aplicación de redes sociales, la nueva Social.NetWork. 

Se trata de una red social de tema libre que constará de miembros, publicaciones y administradores. Tanto los miembros como administradores comparten los siguientes atributos: un email y una contraseña, ambos serán utilizados para verificar su identidad y acceder a la aplicación en el futuro. De los miembros se conoce además el nombre, apellido, fecha de nacimiento y la lista de amigos. 

Un miembro le puede solicitar a otro establecer un vínculo de amistad, generando una invitación, de la cual se conoce un id, el miembro solicitante, el miembro solicitado, el estado de esa solicitud que puede tomar valores PENDIENTE_APROBACION, APROBADA O RECHAZADA y la fecha de solicitud. La fecha de solicitud será la fecha actual. 

Cuando el miembro acepta la solicitud de amistad, se establece un vínculo recíproco, es decir que el vínculo de amistad se genera para los dos. Un miembro puede estar bloqueado por el administrador y en ese caso se verán restringidas algunas funcionalidades. Si el miembro está bloqueado no podrá ni enviar solicitudes ni aceptarlas/rechazarlas.

Los miembros son los únicos capaces de realizar publicaciones, las que contienen un identificador único secuencial, un texto no vacío, una fecha (la fecha actual) y el autor (es decir, el mismo miembro que la publicó). 

Existen dos tipos de publicaciones: Los Posts y los comentarios a un Post. De cada uno se conoce un id, el título (no vacío, al menos de 3 caracteres), el autor, la fecha, el contenido que no puede ser vacío. 

Los Posts tienen además una imagen obligatoriamente (en esta etapa solo se guardará el nombre de la imagen, que no puede ser vacío y debe terminar en ".jpg" o ".png"), pueden ser públicos o privados, tienen una lista de comentarios y pueden estar censurados por el administrador, y en ese caso están deshabilitados, no se pueden mostrar ni comentar. Los posts privados solo podrán ser visualizados y comentados por los amigos de su autor.

Los comentarios no tendrán ningún tipo de publicación anidada. Los comentarios a un post privado serán privados, y los correspondientes a un post público serán públicos. 

Las publicaciones, tanto Posts y comentarios, pueden tener reacciones de las que interesa saber el tipo de reacción (Like o Dislike) y el miembro que lo realizó. Un usuario puede poner un solo like/dislike por publicación.




De las publicaciones nos interesa conocer/calcular el valor de aceptación (VA) cuya fórmula varía dependiendo del tipo de publicación. Veamos: 
• Para calcular el VA de un Post, se cuentan todas las reacciones del tipo "Like" y se multiplica por un Factor de VA (FVA) igual a 5. Este mismo cálculo se realiza para "Dislikes" pero multiplicado por un · FVA igual a -2. Sumamos ambas partes y por último se suma 10 si el Post es público, de lo contrario 0.
• Para comentarios se realiza el mismo cálculo, pero sin incluir los 10 puntos extras. Lo que quedaría: 
VA = (Likes x 5) + (Dislikes x −2)
IMPORTANTE: Para esta entrega no se solicita realizar el método que permite determinar el valor de aceptación de un POST, pero debe estar diseñado y presente en el diagrama de clases. 

Se pide: 

Punto 1: Diseño 

Diseño de la realidad planteada representada en el diagrama de clases completo del Dominio (Reglas del negocio) que modele la situación anterior. Se seguirá el estándar UML y debe ser presentado en formato Astah. 

Punto 2: Implementación 

Implementar al menos dos proyectos 1) biblioteca de clases y 2) aplicación de consola - que incluyan el código que corresponda - en Visual Studio 2022 usando .NET 7 y C# como lenguaje de programación, que incluya: 

Codificación de las clases del dominio necesarias para cumplir con todos los requerimientos del sistema solicitados para este obligatorio (atributos, propiedades, constructor/es, ToString, validaciones y métodos necesarios para llevar adelante los requerimientos planteados). 

Precarga de datos en el sistema para que permita hacer pruebas con distintos escenarios. Se deberá implementar como mínimo la precarga de:
10 miembros 
1 administrador
Invitaciones para todos los miembros: donde 2 de ellos tengan al menos un vínculo de amistad con cada uno de los demás miembros restantes y además deberá precargar invitaciones en todos los estados posibles. (aprobadas, rechazadas y en proceso).
5 posts y al menos 3 comentarios para cada uno.
Reacciones para al menos 2 post, y para al menos 2 comentarios. 
       

3. Desplegar un menú en consola que permita:
Alta de miembro. Recuerde verificar que se cumplan las reglas mencionadas en la realidad planteada.
Dado un email de miembro listar todas las publicaciones que ha realizado, diferenciando en la lista su tipo (si es post o comentario).
Dado un email de miembro, listar los posts en los que haya realizado comentarios. Se listarán solamente los posts, no se incluirán los comentarios.
Dadas dos fechas listar los posts realizados entre esas fechas inclusive. Se deberá mostrar id, fecha, título y el texto del post. Si el texto del post supera los 50 caracteres, solo se mostrarán los primeros 50. No se mostrarán los comentarios de dichos posts. El listado estará ordenado por título en forma descendente.
Obtener los miembros que hayan realizado más publicaciones de cualquier tipo. Si hay más de un miembro con la misma cantidad de publicaciones mostrarlos todos. Se mostrarán todos los datos de los miembros. 
----------------------------------------------------------------------------------------
