﻿Public Class MarcaDAO
    ' DOC: (MarcaDataAccessObject) Esta clase se encarga de las consultas SQL a la tabla de Marcas.
    '      Como parametros de entrada/salida generalmente trabaja con MarcaVO.

    ' TODO: Modificar BD para que el campo Nombre sea UNIQUE
    Public Shared Function all() As DataTable
        Dim sql_select = "SELECT idMarca, nombre FROM marcas"
        Return DataBase.getInstance().consulta_sql(sql_select)
    End Function

    Public Shared Sub insert(ByRef marca As MarcaVO)
        ' DOC: Inserta la marca en la BD y actualiza el objeto asignandole su ID.

        Dim sql_insertar As String
        sql_insertar = "INSERT INTO marcas (nombre)"
        sql_insertar &= " VALUES ("
        sql_insertar &= "'" & marca.get_nombre() & "')"
        sql_insertar &= "; SELECT SCOPE_IDENTITY()" ' Retorna el ID de la fila insertada.
        Dim tabla = DataBase.getInstance().consulta_sql(sql_insertar)
        marca.set_id(tabla(0)(0))
    End Sub

    Public Shared Sub update(ByRef marca As MarcaVO)
        ' TODO: Update a marca en la BD.
        Dim sql_update As String
        sql_update = "UPDATE marcas"
        sql_update &= " SET "
        sql_update &= "nombre='" & marca.get_nombre() & "'"
        sql_update &= " WHERE idMarca=" & marca.get_id()
        DataBase.getInstance().ejecuta_sql(sql_update)
    End Sub

    Public Shared Sub save(ByRef marca As MarcaVO)
        ' DOC: Si la marca existe actualiza, sino inserta.
        If marca.is_in_bd() Then
            MarcaDAO.update(marca)
        Else
            MarcaDAO.insert(marca)
        End If
    End Sub

    Public Shared Sub delete(ByRef marca As MarcaVO)
        ' TODO: Validar que tenga un ID (esta en la BD) y eliminar la marca.
        If marca.is_in_bd() Then
            Dim sql_delete = "DELETE FROM marcas"
            sql_delete &= " WHERE idMarca=" & marca.get_id()
            DataBase.getInstance().ejecuta_sql(sql_delete)
            marca.set_id(0)
        End If
    End Sub






End Class
