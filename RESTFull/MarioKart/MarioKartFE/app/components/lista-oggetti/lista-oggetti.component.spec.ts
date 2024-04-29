import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaOggettiComponent } from './lista-oggetti.component';

describe('ListaOggettiComponent', () => {
  let component: ListaOggettiComponent;
  let fixture: ComponentFixture<ListaOggettiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListaOggettiComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListaOggettiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
