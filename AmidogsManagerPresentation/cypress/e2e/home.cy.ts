describe('Prueba de la Navegación y la Página de Chats', () => {
    it('debería mostrar la página de inicio y luego navegar a la página de chats', () => {
      // Visita la página de inicio
      cy.visit('http://localhost:8100/tabs/home');  // Asegúrate de que esta URL sea la correcta para tu aplicación
  
      // Verifica que el título de la página de inicio sea 'Amidogs'
      cy.get('ion-title').should('contain.text', 'Amidogs');
  
      // Supongamos que tienes un botón para navegar a la página de chats
      // Cambia el selector según el botón o enlace en tu aplicación
      cy.get('ion-tab-button[tab="chats"]').click();
  
      // Verifica que la navegación haya sucedido correctamente
      cy.url({ timeout: 10000 }).should('include', '/tabs/chats'); // Asegúrate de que esta URL sea la correcta
  
      // Verifica que el título de la página de chats sea 'Chats'
      cy.get('ion-title').should('contain.text', 'Chats');

      // Verifica la presencia de elementos esperados en la página de chats
      // Verifica la presencia de chats nuevos

      cy.wait(14000); // Espera 2 segundos
      cy.get('.horizontal-slider').should('exist');
      cy.get('.slider-item').should('have.length.greaterThan', 0);
  
      // Verifica la presencia de chats empezados
      cy.get('ion-list').should('exist');
      cy.get('ion-item').should('have.length.greaterThan', 0);
    });
  });
  