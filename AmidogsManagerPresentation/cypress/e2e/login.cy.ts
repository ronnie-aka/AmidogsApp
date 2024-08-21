describe('Prueba de Inicio de Sesión', () => {
  beforeEach(() => {
    cy.viewport(412, 915);
  });

  it('debería permitir a un usuario iniciar sesión con credenciales válidas', () => {
    // Indicamos la URL a la que se debe navegar.
    cy.visit('http://localhost:8100');

    // Escribe en el campo de email
    cy.get('ion-input[placeholder="Email"] input').type(
      'ronald.hernandez@a.maux.org'
    );

    // Escribe en el campo de contraseña
    cy.get('ion-input[placeholder="Contraseña"] input').type(
      'contraseñaEjemplo'
    );

    // Ordenamos hacer click en el botón de inicio de sesión
    cy.get('ion-button.login-button').click();

    // Verifica que la URL haya cambiado después del inicio de sesión
    // Añadimos un pequeño timeOut para dar tiempo a que la petición se cargue
    cy.url({ timeout: 10000 }).should('include', '/tabs/home');

    // Verifica que el <ion-title> contenga el texto 'Amidogs']
    cy.get('ion-title').should('contain.text', 'Amidogs');

    // Verifica que al menos un <app-card> esté presente
    cy.get('app-card').should('exist'); 
  });
});
