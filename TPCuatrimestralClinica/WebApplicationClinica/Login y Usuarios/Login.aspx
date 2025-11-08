<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplicationClinica.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="min-vh-100 d-flex align-items-center justify-content-center">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-12 col-md-8 col-lg-6">
                    <div class="card shadow-sm border-0">
                        <div class="card-body p-4 p-md-5">
                            <h1 class="h4 text-center mb-4">Iniciar sesión</h1>
                            <!-- Usuario -->
                            <div class="mb-3 row">
                                <label for="inputUser" class="col-sm-3 col-form-label text-sm-end">Usuario</label>
                                <div class="col-sm-6">
                                 <asp:TextBox ID="TxtUsuario" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                            </div>

                            <!-- Contraseña ayuda debajo del input -->
                            <div class="mb-3 row">
                                <label for="inputPassword" type="password" class="col-sm-3 col-form-label text-sm-end">Contraseña</label>
                                <div class="col-sm-6">

                                    <div class="input-group">
        
                                        <asp:TextBox ID="TxtPassword" type="password" CssClass="form-control" runat="server"></asp:TextBox>
        
                                        <button type="button" class="btn btn-outline-secondary" id="btnMostrarPass" onclick="mostrarOcultarPassword()">👁</button>

                                    </div><div id="passwordHelp" class="form-text">Must be 8–20 characters long.</div>
    


                                </div>

                            </div>

                            <!-- Botón Iniciar sesión -->
                            <div class="row">
                                <div class="offset-sm-4 col-sm-4">
                                    <div class="d-grid">


                                        <asp:Button ID="BtnIngresar" cssclas="btn btn-primary" runat="server" Text="Ingresar" OnClick="BtnIngresar_Click" />

                                        <asp:Label ID="LblCargar" runat="server" Text=""  CssClass="alert alert-danger mt-3 text-center fw-semibold shadow-sm"  ></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script>
        function mostrarOcultarPassword() {
            const txt = document.getElementById('<%= TxtPassword.ClientID %>');
            const btn = document.getElementById('btnMostrarPass');

            if (txt.type === 'password') {
                txt.type = 'text';
                btn.textContent = '🙈';  // ahora se ve
            } else {
                txt.type = 'password';
                btn.textContent = '👁️';  // vuelve a ocultar
            }
        }
    </script>
</asp:Content>
