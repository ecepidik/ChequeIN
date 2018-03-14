import 'jest-preset-angular';
import 'rxjs/Rx';

/*** Mocks ***/
Object.defineProperty(document.body.style, 'transform', {
  value: () => {
    return {
      enumerable: true,
      configurable: true,
    };
  },
});

Object.defineProperty(window, 'CSS', {value: jest.fn()});
